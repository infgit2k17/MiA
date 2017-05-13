// Write your Javascript code.

$(document).ready(function () {
    $("#departing").datepicker();
    $("#returning").datepicker();
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