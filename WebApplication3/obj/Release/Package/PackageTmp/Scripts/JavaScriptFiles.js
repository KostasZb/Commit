//Script that adds the photos to the generated causes elements
$(document).ready(function () {
    $(".Other").attr("src", "/Content/CausesPhotos/otherPhoto.png");
    $(".TaxAvoidance").attr("src", "/Content/CausesPhotos/taxPhoto.png");
    $(".Racism").attr("src", "/Content/CausesPhotos/racism.png");
    $(".UnfairEmployment").attr("src", "/Content/CausesPhotos/employment.png");
    $(".Environment").attr("src", "/Content/CausesPhotos/environment.png");
});

//Script that makes an Ajax call and sends the the added signature in the causeDetails page and updates the DB, using the CausesController
$(document).ready(function () {
    $("#signButton").on("click", function () {
        var idMember = $(".memberNavBar").attr("id");
        if (idMember != null) {
            var idString = $(".causeDetails").attr("id");
            $.ajax({
                type: "post",
                url: "/Causes/addSignature",
                data: { idString: idString, idMember: idMember },
                datatype: "text",
                success: function (data) {
                    alert(data)
                }
            })
        }
    });
});

//Ajax call for SignUp, using the UsersController
$(document).ready(function () {
    $("#signUpButton").on("click", function () {
        var member = {};
        member.firstName = $("#fName").val();
        member.lastName = $("#lName").val();
        member.email = $("#emailSignUp").val();
        member.password = $("#passwordSignUp").val();
        $.ajax({
            type: "POST",
            url: "Users/SignUp",
            data: member,
            dataType: "text",
            success: function (data) {
                alert(data);
                $("input").val("");
            }
        });
    });
});

//Ajax call for login,using the UsersController
$(document).ready(function () {
    $("#logInButton").on("click", function () {
        passwordLogIn = $("#passwordLogIn").val();
        emailLogIn = $("#emailLogIn").val();
        $.ajax({
            type: "POST",
            url: "Users/LogIn",
            data: { emailLogIn: emailLogIn, passwordLogIn: passwordLogIn },

            success: function (data) {

                if (data.result == "logInSuccess") {

                    window.location = data.url;
                } else {
                    $("input").val("");
                    alert("Email and password not matching, please try again")
                }
            }
        });
    });
});

//performing a lower/uppercase independent seach based on the keyword entered by the user on the Causes Index page
$(document).ready(function () {
    $("#searchBar").on("click", function () {
        $("#causeTypeDropdown").val("");
    });
    $("#searchBar").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#causeCard ").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
});

//filtering the Causes based on selection of cause type from dropdown menu in the Causes Index page
$(document).ready(function () {
    $("#causeTypeDropdown").on("change", function () {
        $("#searchBar").val("");
        var selectedItem = $(this).val();
        $("#causeCard ").filter(function () {
            var identity = $(this).find("img").attr("id");
            $(this).toggle(identity == selectedItem);
        });
    });
});


//Using Ajax to update the signature count and the members signed, it updates every 5sec with the newest data 
$(document).ready(function () {
    setInterval(function () { realtimeUpdate() }, 5000);
});
function realtimeUpdate() {
    
    $.ajax({
        type: "post",
        url: "/Causes/realTimeUpdate",
        dataType: "json",
        success: function (data) {
            $(".peopleSignedList").empty();
            $.each(data, function (i, cause) {
                $(".signatureCount").each(function () {
                    if ($(this).attr("id") == cause.id) {
                        $(this).text(cause.PeopleSigned.length + " Commited!")
                    }
                })
                if ($(".peopleSignedList").attr("id") == cause.id) {
                    for (var counter = 0; counter < cause.PeopleSigned.length; counter++) {
                        var memberName = cause.PeopleSigned[counter].firstName + " " + cause.PeopleSigned[counter].lastName;
                        var htmlToAppend = '<li class=" list-group-item border-0">' + memberName + '</li>';
                        $(".peopleSignedList").append(htmlToAppend);
                }
                }
            });
        }
    });
} 
