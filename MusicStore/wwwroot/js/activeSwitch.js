document.addEventListener("DOMContentLoaded", function ()
{
            var isActiveSwitch = document.getElementById("isActiveSwitch");
            var isActiveLabel = document.getElementById("isActiveLabel");

            // Ustawienie stanu checkboxa na podstawie wartości z modelu
            if (@Model.isActive.ToString().ToLower()) {
                isActiveSwitch.checked = true;
                isActiveSwitch.disabled = true;
                isActiveLabel.innerText = "Field is activated and can be only disabled by deleting item";
            }

            // Dodanie event listenera, aby zapobiec wyłączaniu, gdy przełącznik jest włączony
            isActiveSwitch.addEventListener("change", function (event) {
                if (@Model.isActive.ToString().ToLower() && !isActiveSwitch.checked) {
                    // Przywróć zaznaczenie przełącznika
                    isActiveSwitch.checked = true;
                    // Ustaw przełącznik jako disabled
                    isActiveSwitch.disabled = true;
                    isActiveLabel.innerText = "This option is active and can only be deactivated by deleting the item";
                }
            });
 });
 