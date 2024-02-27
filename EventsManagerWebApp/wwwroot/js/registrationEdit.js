var btnEdits = document.querySelectorAll(".btnEdit");

btnEdits.forEach(function (btnEdit, index) {
    btnEdit.addEventListener("click", function () {
        
        var dataIndex = this.getAttribute("data-index");
        //hide all lables
        var lblParticipantName = document.getElementById('lblParticipantName' + dataIndex);
        lblParticipantName.style.display = "none";
        var lblParticipantEmail = document.getElementById('lblParticipantEmail' + dataIndex);
        lblParticipantEmail.style.display = "none";

        //show all inputs
        var txtParticipantName = document.getElementById('txtParticipantName' + dataIndex);
        txtParticipantName.classList.remove("invisible");
        var txtParticipantEmail = document.getElementById('txtParticipantEmail' + dataIndex);
        txtParticipantEmail.classList.remove("invisible");


        //hide this button
        btnEdit.style.display = "none";
        //show submit button
        var tntSave = document.getElementById('tntSave' + dataIndex);
        tntSave.classList.remove("btnSave");
        document.getElementById('Form' + dataIndex).onsubmit = function (event) {
            event.preventDefault();

            // verify participantName
            if (txtParticipantName.value.trim() === '') {
                alert('Invalid participant name!');
                txtParticipantName.focus();
                return;
            }

            //verify email address
            var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            if (!emailRegex.test(txtParticipantEmail.value)) {
                alert('Invalid email address!');
                txtParticipantEmail.focus();
                return;
            }

            //everything is fine and continue to submit
            document.getElementById('Form' + dataIndex).submit();
        }

    });

    
    
});

