//check client edit before submit 
document.getElementById('FormAddANewEvent').onsubmit = function (event) {
    event.preventDefault(); 
    // verify event tName
    var txtNewEventName = document.getElementById('txtNewEventName');
    if (txtNewEventName.value.trim() === '') {
        alert('Invalid event name!');
        txtNewEventName.focus();
        return;
    }

    //verify event time
    var txtEventDate = document.getElementById('txtEventDate');
    var eventTime = txtEventDate.value;
    if (eventTime.trim() === '') {
        alert('Invalid event date and time!');
        txtEventDate.focus();
        return;
    } else {
        var inputDate = new Date(eventTime);
        var now = new Date();
        if (inputDate < now) {
            alert('Invalid event date and time!\nIt must be later than now!');
            txtEventDate.focus();
            return;
        }
    }

    //verify Event Location
    var newEventLocation = document.getElementById('txtEventLocation');
    if (newEventLocation.value.trim() === '') {
        alert('Invalid event location!');
        newEventLocation.focus();
        return;
    }

    //everything is fine and continue to submit
    document.getElementById('FormAddANewEvent').submit();
}
