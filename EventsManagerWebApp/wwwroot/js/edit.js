//check client edit before submit 
document.getElementById('EditForm').onsubmit = function (event) {
    event.preventDefault(); 
    var isAbleToSubmit = true;
    var eventName = document.getElementsByName('newEventName')[0].value;
    var eventDate = document.getElementsByName('newEventDate')[0].value;
    var eventLocation = document.getElementsByName('newEventLocation')[0].value;
    var lblNewEventName = document.getElementById('lblNewEventName');
    var lblNewEventDate = document.getElementById('lblNewEventDate');
    var lblNewEventLocation = document.getElementById('lblNewEventLocation');
    

    // Check eventName
    if (eventName.trim() === '') {
        lblNewEventName.innerHTML = 'Event name must not be empty.';
        isAbleToSubmit = false;
    } else {
        lblNewEventName.innerHTML = '*';
    }

    // Check eventLocation
    if (eventLocation.trim() === '') {
        lblNewEventLocation.innerHTML = 'Event location must not be empty.';
        isAbleToSubmit = false;
    } else {
        lblNewEventLocation.innerHTML = '*';
    }

    // Check event date and time, it must not be empty and later than now
    var inputDate = new Date(eventDate);
    var currentDate = new Date();
    if (inputDate <= currentDate || eventDate.trim() === '') {
        lblNewEventDate.innerHTML = 'Date must not be empty and later than now!';
        isAbleToSubmit = false;
    } else {
        lblNewEventDate.innerHTML = '*';
    }

    // If isAbleToSubmit is true, submit the form programmatically
    if (isAbleToSubmit) {
        document.getElementById('EditForm').submit();
    }
}
