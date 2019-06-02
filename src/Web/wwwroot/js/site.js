// Write your JavaScript code.
$(function(){
    $('[data-toggle="tooltip"]').tooltip();
    $('[data-toggle="popover"]').popover({
        placement: 'bottom',
        content: function(){
            return $("#notification-content").html();
        },
        html: true
    });

    $('body').append(`<div id="notification-content" style="display: none;"></div>`)
    

    function getNotification(){
        var res = "<ul class='list-group'>";
        $.ajax({
            url: "/Tenantarea/Notification/GetNotification",
            method: "GET",
            success: function(result){

                if(result.count!=0){
                $("#notificationCount").html(result.count);
                $("#notificationCount").show('slow');
                } else {
                    $("#notificationCount").html();
                    $("#notificationCount").hide('slow');
                    $("#notificationCount").popover('hide');                    
                }
                
                var notifications = result.userNotification;
                notifications.forEach(element => {
                    res = res + "<li class='list-group-item notification-text' style='color:#fff;background-color:#1d6ac8;background-image:-moz-linear-gradient(#45b3f3, #1d6ac8);background-image:-webkit-linear-gradient(#45b3f3, #1d6ac8);border-color:#1d6ac8 #1d6ac8 #1a5eb2' id='donedone' data-id='"+element.notificationId+"'>"+element.notification.text+" </li>";
                });

                res = res + "</ul>";

                $("#notification-content").html(res);

                //console.log(result);
            },
            error: function(error){
                console.log(error);
            }
        });
    }
    
    $(document).on('click','.list-group-item ',function(e){
        var target = e.target;
        var id = $(target).data('id');
        
        readNotification(id,target);
    })


    function readNotification(id,target){
        $.ajax({
            url: "/Tenantarea/Notification/ReadNotification",
            method: "GET",
            data : {notificationId : id},
            success : function(result){
                getNotification();
                $(target).fadeOut('slow');
            },
            error: function(error){
                console.log(error);
            }
        })
    }



    getNotification();


});

//"/Tenantarea/Notification/GetNotification"