// sw.js - نسخه نهایی با رفع مشکل sync

const STATIC_CACHE = "static_v2";
const DYNAMIC_CACHE = "dynamic_v2";

const STATIC_PAGES = ["/offline.html"];

const STATIC_ASSETS = [
    "SiteTemplate/css/normalize.css",
    "SiteTemplate/css/swiper-bundleM.css",
    "SiteTemplate/css/swiper-bundle.min.css",
    "SiteTemplate/css/fancybox.css",
    "SiteTemplate/css/swiper.css",
    "SiteTemplate/css/custom.css",
    "SiteTemplate/css/added-new.css",
    "SiteTemplate/js/scroll.js",
    "SiteTemplate/js/jquery.js",
    "SiteTemplate/js/swiper-bundle.min.js",
    "SiteTemplate/js/preview.js",
    "SiteTemplate/js/script.js",
    "SiteTemplate/js/app.js",
    "SiteTemplate/js/sweet-alert.js",
    "SiteTemplate/js/fancybox.js",
    "SiteTemplate/js/swiper-bundleList.min.js",
    "SiteTemplate/js/custom.js",
    "SiteTemplate/js/added-new.js",
    "/offline.html"
];

// ========== INDEXED DB در سرویس ورکر ==========
const DB_NAME_SW = 'pwa_data_store';
const STORE_NAME_SW = 'subscribe_requests';
let dbInstanceSW = null;

function getDbSW() {
    return new Promise((resolve, reject) => {
        if (dbInstanceSW) {
            return resolve(dbInstanceSW);
        }
        const request = self.indexedDB.open(DB_NAME_SW, 2);

        request.onupgradeneeded = (event) => {
            const db = event.target.result;
            if (!db.objectStoreNames.contains(STORE_NAME_SW)) {
                db.createObjectStore(STORE_NAME_SW, { keyPath: 'id', autoIncrement: true });
                console.log('[SW DB] Object store created');
            }
        };

        request.onsuccess = (event) => {
            dbInstanceSW = event.target.result;
            console.log('[SW DB] Database opened successfully');
            resolve(dbInstanceSW);
        };

        request.onerror = (event) => {
            console.error('[SW DB] Error opening database:', event.target.error);
            reject(event.target.error);
        };
    });
}

async function getAllSubscribeRequestsSW() {
    const db = await getDbSW();
    return new Promise((resolve, reject) => {
        const transaction = db.transaction([STORE_NAME_SW], 'readonly');
        const store = transaction.objectStore(STORE_NAME_SW);
        const request = store.getAll();

        request.onsuccess = () => {
            console.log(`[SW DB] Found ${request.result.length} pending requests`);
            resolve(request.result);
        };
        request.onerror = (event) => {
            console.error('[SW DB] Error getting all requests:', event.target.error);
            reject(event.target.error);
        };
    });
}

async function deleteSubscribeRequestSW(id) {
    const db = await getDbSW();
    return new Promise((resolve, reject) => {
        const transaction = db.transaction([STORE_NAME_SW], 'readwrite');
        const store = transaction.objectStore(STORE_NAME_SW);
        const request = store.delete(id);

        request.onsuccess = () => {
            console.log('[SW DB] Subscribe request deleted:', id);
            resolve();
        };
        request.onerror = (event) => {
            console.error('[SW DB] Error deleting request:', event.target.error);
            reject(event.target.error);
        };
    });
}

// ========== تابع همگام‌سازی ==========
async function syncSubscribeFormData() {
    console.log('[SW] Starting background sync...');
    const allRequests = await getAllSubscribeRequestsSW();

    if (allRequests.length === 0) {
        console.log('[SW] No pending requests to sync');
        return;
    }

    console.log(`[SW] Syncing ${allRequests.length} request(s)`);

    for (const req of allRequests) {
        try {
            const response = await fetch('/Subscribe', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ mobile: req.mobile })
            });

            if (response.ok) {
                console.log('[SW] Synced mobile:', req.mobile);
                await deleteSubscribeRequestSW(req.id);

                self.registration.showNotification('ثبت‌نام موفق', {
                    body: `شماره ${req.mobile} با موفقیت عضو شد.`,
                    icon: '/icon-192x192.png'
                });
            } else {
                console.error('[SW] Server error for:', req.mobile);
            }
        } catch (error) {
            console.error('[SW] Network error for:', req.mobile, error);
            break;
        }
    }
}

// ========== INSTALL ==========
self.addEventListener("install", (event) => {
    console.log("[SW] Installing…");
    event.waitUntil(
        caches.open(STATIC_CACHE).then((cache) => {
            console.log("[SW] Caching static assets");
            return cache.addAll([...STATIC_PAGES, ...STATIC_ASSETS]);
        }).catch(err => {
            console.error("[SW] Cache addAll failed:", err);
        })
    );
    self.skipWaiting();
});

// ========== ACTIVATE ==========
self.addEventListener("activate", (event) => {
    console.log("[SW] Activating…");
    event.waitUntil(
        caches.keys().then((keys) =>
            Promise.all(
                keys.map((key) => {
                    if (key !== STATIC_CACHE && key !== DYNAMIC_CACHE) {
                        console.log('[SW] Deleting old cache:', key);
                        return caches.delete(key);
                    }
                })
            )
        ).then(() => {
            console.log("[SW] Claiming clients");
            return self.clients.claim();
        })
    );
});

// ========== FETCH EVENT (یکپارچه) ==========
self.addEventListener("fetch", (event) => {
    const url = new URL(event.request.url);
    const { request } = event;

    // مدیریت POST /Subscribe
    if (request.method === "POST" && url.pathname === '/Subscribe') {
        event.respondWith(handleSubscribePost(event.request));
        return;
    }

    // فقط GET درخواست‌ها
    if (request.method !== "GET") return;

    // درخواست‌های استاتیک
    if (STATIC_ASSETS.some(asset => url.pathname.includes(asset)) ||
        STATIC_PAGES.includes(url.pathname)) {
        event.respondWith(
            caches.match(request).then((cached) => {
                return cached || fetch(request).then((res) => {
                    return caches.open(DYNAMIC_CACHE).then((cache) => {
                        cache.put(request, res.clone());
                        return res;
                    });
                }).catch(() => {
                    return caches.match("/offline.html");
                });
            })
        );
        return;
    }

    // سایر درخواست‌ها
    event.respondWith(
        fetch(request).then((res) => {
            if (res && res.status === 200 && request.method === "GET") {
                const resClone = res.clone();
                caches.open(DYNAMIC_CACHE).then((cache) => {
                    cache.put(request, resClone);
                });
            }
            return res;
        }).catch(() => {
            return caches.match(request).then((cached) => {
                return cached || caches.match("/offline.html");
            });
        })
    );
});

// ========== NOTIFICATION CLICK ==========
self.addEventListener('notificationclick', (event) => {
    event.notification.close();
    event.waitUntil(
        clients.matchAll({ type: 'window' }).then((clientList) => {
            for (const client of clientList) {
                if (client.url === '/' && 'focus' in client) {
                    return client.focus();
                }
            }
            if (clients.openWindow) {
                return clients.openWindow('/');
            }
        })
    );
});