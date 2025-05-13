var productDetailsImages = document.querySelectorAll(".product-details-image");
var productDetailsImagesContainers = document.querySelectorAll(
    ".product-details-image-container"
);
var productDetailsThumbnail = document.getElementById(
    "product-details-thumbnail"
);

if (productDetailsImages && productDetailsThumbnail) {
    for (let i = 0; i < productDetailsImages.length; i++) {
        productDetailsImages[i].addEventListener("click", () => {
            productDetailsThumbnail.src = productDetailsImages[i].src;

            for (let j = 0; j < productDetailsImages.length; j++) {
                productDetailsImages[j].setAttribute("data-selected", "false");
                productDetailsImagesContainers[j].style.border = "1px solid #D1D5DB";
            }

            productDetailsImages[i].setAttribute("data-selected", "true");

            productDetailsImagesContainers[i].style.border = "2px solid #9CA3AF";
        });
    }
}
