document.addEventListener("DOMContentLoaded", function () {
    const iocnLinks = document.querySelectorAll(".iocn-link");
    const subMenus = document.querySelectorAll(".sub-menu");

    iocnLinks.forEach((arrow) => {
        arrow.addEventListener("click", function () {
            const parentListItem = arrow.closest("li");
            const isOpen = parentListItem.classList.contains("showMenu");

            // Close all submenus except the one clicked
            subMenus.forEach((submenu) => {
                const submenuParent = submenu.parentElement;
                if (submenu !== parentListItem.querySelector('.sub-menu')) {
                    submenuParent.classList.remove("showMenu");
                    submenu.style.height = '0';
                }
            });

            // Toggle the clicked submenu
            if (!isOpen) {
                parentListItem.classList.add("showMenu");
                const currentSubMenu = parentListItem.querySelector('.sub-menu');
                currentSubMenu.style.height = currentSubMenu.scrollHeight + 'px';
            } else {
                parentListItem.classList.remove("showMenu");
                const currentSubMenu = parentListItem.querySelector('.sub-menu');
                currentSubMenu.style.height = currentSubMenu.scrollHeight + 'px';
            }
        });

        const submenuItems = arrow.nextElementSibling.querySelectorAll('li');

        submenuItems.forEach((item) => {
            item.addEventListener('click', function () {
                // Remove active class from all submenu items
                submenuItems.forEach((subitem) => {
                    subitem.querySelector('.link-name').classList.remove('active');
                });

                // Add active class to the clicked submenu item
                item.querySelector('.link-name').classList.add('active');
            });
        });

    });
});
