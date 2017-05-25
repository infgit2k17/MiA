// Write your Javascript code.

$(document).ready(function () {
    $("datepicker").datepicker({ format: 'dd/mm/yyyy', autoclose: true, todayBtn: 'linked' })
    $("button").click(function () {
        var selected = $("#dropdown option:selected").text();
        var departing = $("#departing").val();
        var returning = $("#returning").val();
        if (departing === "" || returning === "") {
            alert("Please select departing and returning dates.");
        } else {
            confirm("Would you like to go to " + selected + " on " + departing + " and return on " + returning + "?");
        }
    });
});


/*var sIndex = 11, offSet = 10, isPreviousEventComplete = true, isDataAvailable = true;
  
    $(window).scroll(function () {
     if ($(document).height() - 50 <= $(window).scrollTop() + $(window).height()) {
      if (isPreviousEventComplete && isDataAvailable) {
       
        isPreviousEventComplete = false;
        $(".LoaderImage").css("display", "block");

        $.ajax({
          type: "GET",
          url: 'getMorePosts.ashx?startIndex=' + sIndex + '&offset=' + offSet + '',
          success: function (result) {
            $(".divContent").append(result);

            sIndex = sIndex + offSet;
            isPreviousEventComplete = true;

            if (result == '') //When data is not available
                isDataAvailable = false;

            $(".LoaderImage").css("display", "none");
          },
          error: function (error) {
              alert(error);
          }
        });

      }
     }
    });*/

//http://www.dotnetbull.com/2013/05/browser-scroll-to-end-of-page-in-jquery.html

$(document).ready(function () {
    $("#departing").datepicker({ format: 'dd/mm/yyyy', autoclose: true, minData: new Date() })
    $("#returning").datepicker({ format: 'dd/mm/yyyy', autoclose: true, minData: new Date() })
    });


function uriaction() {
    window.location.href = 'Home/Search?Destination=' + $("#destin").val() + '&Arrival=' + $("#departing").val() + '&Departure=' + $("#returning").val() + '&Guests=' + $("#count_picker").val();
}

function uriaction2() {
    window.location.href = 'Search?Destination=' + $("#destin").val() + '&Arrival=' + $("#departing").val() + '&Departure=' + $("#returning").val() + '&Guests=' + $("#count_picker").val();
}


//Odpoiwiedzialne za wyświetlanie modal
var modal = document.getElementById('myModal');
var btn = document.getElementById("myBtn");
var span = document.getElementsByClassName("close")[0];
btn.onclick = function () {
    modal.style.display = "block";
}
span.onclick = function () {
    modal.style.display = "none";
}
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}


