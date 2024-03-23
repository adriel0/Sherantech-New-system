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
    });
});


function openTab(evt, tabName) {
    // Declare all variables
    var i, tabcontent, tablinks;

    // Get all elements with class="tabcontent" and hide them
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }

    // Get all elements with class="tablinks" and remove the class "active"
    //tablinks = document.getElementsByClassName("tablinks");
    //for (i = 0; i < tablinks.length; i++) {
    //    tablinks[i].className = tablinks[i].className.replace(" active", "");
    //}
    
    // Show the current tab, and add an "active" class to the button that opened the tab
    document.getElementById(tabName).style.display = "block";
    // Get the height of the upper body
    const upperBodyHeight = document.getElementById(tabName).children[0].children[0].offsetHeight;

    // Set the top position of the lower body
    const lowerBody = document.querySelector('.lower-body');
    lowerBody.style.top = `${upperBodyHeight + 90}px`;
    //evt.currentTarget.className += " active";
}

function uncheckOthers(currentCheckbox) {
    var checkboxes = document.querySelectorAll('.check-space');
    checkboxes.forEach(function(checkbox) {
        if (checkbox !== currentCheckbox) {
            checkbox.checked = false;
        }
    });
}