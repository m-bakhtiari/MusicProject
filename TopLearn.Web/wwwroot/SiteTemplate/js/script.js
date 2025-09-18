window.addEventListener("DOMContentLoaded", () => {
	const items = [...document.querySelectorAll(".list-images__item")];
	const imageBox = document.querySelector(".list-images__image-box");

	let targetX = 0,
		targetY = 0,
		currentX = 0,
		currentY = 0;

	// Image Animation

	const animateImage = () => {
		currentX += (targetX - currentX) * 0.1;
		currentY += (targetY - currentY) * 0.1;

		imageBox.style.transform = `translate3d(${currentX}px, ${
			currentY - 20
		}px, 0) rotate(4deg)`;

		requestAnimationFrame(animateImage);
	};

	animateImage();

	// Show Image

	const showImage = (e) => {
		const cursorX = e.clientX;
		const cursorY = e.clientY;

		targetX = cursorX;
		targetY = cursorY;

		let hovered = false;

		items.forEach((item) => {
			const itemPlace = item.getBoundingClientRect();

			const isHovered =
				cursorX >= itemPlace.left &&
				cursorX <= itemPlace.right &&
				cursorY >= itemPlace.top &&
				cursorY <= itemPlace.bottom;

			if (isHovered) {
				const imgUrl = item.getAttribute("data-img");
				const oldImage = imageBox.querySelector("img");

				if (oldImage) oldImage.remove();

				const newImage = document.createElement("img");

				imageBox.appendChild(newImage);
				imageBox.classList.add("active");
				newImage.src = imgUrl;

				hovered = true;
			}
		});

		if (!hovered) imageBox.classList.remove("active");
	};

	document.addEventListener("mousemove", showImage, false);
});
