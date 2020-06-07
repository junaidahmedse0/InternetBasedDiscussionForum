
    $(document).ready(function () {
        $(document).ready(function () {

            $("#myInput").on("keyup", function () {
                var value = $(this).val().toLowerCase();
                $("#myTable tr").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });
            });
        });
      
    });


    function Add(id) {

        $.ajax({
            type: "POST",
            url: "/Admin/Update?id=" + id,

            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if(response.status==true)
                {
                    window.location = '/Admin/ApproveStudent';
                }
            },
            error: function (response) {

                console.log(response);
              
             }
        });

    };

    function ApprovePost(id) {


        $.ajax({
            type: "POST",
            url: "/Posts/Update?id=" + id,

            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    window.location = '/Admin/ApprovePost';
                }
            },
            error: function (response) {

                console.log(response);
     
            }
        });

    };

  
    function AddPost() {

        var post = {
            
            Question: $("#post-question").val(),
            Subject: $("#post-subject").val(),
            Description: $("#post-description").val(),
           



        }
     
        if (!post.Question) {
            $(".question-label").empty();
            $(".question-label").text("Value is Required").css("color", "red");
        }
      
        if (!post.Description) {
            $(".description-label").empty();
            $(".description-label").text("Value is Required").css("color", "red");;

        }
          else {
            $.ajax({
                type: "POST",
                url: "/Posts/Create",
                data: JSON.stringify(post),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.status == true) {
                         $("#post-question").val(""),
              
                        $("#post-description").val(""),
                        $("#myModal").modal('hide');
                        //window.location = '/Posts/index';
                        $.toast({
                            heading: 'Success',
                            text: 'Your Add post Request Goes To Admin',
                            icon: 'success',
                            position: 'bottom-right'
                        })

                        //window.location = '/Posts/index';
                    }

                },
                error: function (response) {

                    console.log(response);

                    alert("error" + response);
                }
            });
        }

        }
    
    function AddComment(id)
    {
 
        var comment = {
            PostId:id,
            Comment1: $("#"+id).val(),
           
            
        }
          $.ajax({
            type: "POST",
            url: "/Posts/AddComment",
            data: JSON.stringify(comment),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
           
                    $.toast({
                        heading: 'Success',
                        text: 'Your Comment is Added',
                        icon: 'success',
                        position: 'bottom-right'
                    })
                    $("#" + id).val('');

                    
                }
            },
            error: function (response) {

                console.log(response);

                alert(response.message);
            }
        });


    }

function ShowPostDetail(id)
{

        $.ajax({
        type: "GET",
        url: "/Posts/Comments?id=" + id,

        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var data="";
            $.each(response, function (key, val) {
                console.log("FirstName" + val.FirstName+"Comment"+val.Comment1);
                //alert("Name"+val.FirstName+"Comment"+val.Comment1);
                //data += '<span class="label label-default" style="padding:8px;margin-top:20px;">'
                //    + val.FirstName + '</span><h6>' + val.Comment1 + "</h6>";

                data += `<ul class="media-list">
                 <li class="media">
                     <a href="#" class="pull-left">
                         <img src="/Images/circle.jpg" alt="" class="img-circle" width="60px;" height="60px;">
                     </a>
                     <div class="media-body">
                         <span class="text-muted pull-right">
                             <small class="text-muted">30 min ago</small>
                         </span>
                         <strong class ="text-success">${val.FirstName}</strong>
                         <p>
                            ${val.Comment1}
                         </p>
                     </div>
                 </li>
               </ul>`;




            });

            $("#commentsection").append(data);
            $('#PostDetail').modal('show');
        },
        error: function (response) {
           alert("error"+response.message);
        }
    });



}

$("#clearmodal").click(function () {

    $('#PostDetail').modal('hide');
    $("#commentsection").empty();
})

function AddLike(id)
{

    $.ajax({
        type: "POST",
        url: "/Posts/AddLike?id=" + id,

        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.status == true) {
                $.toast({
                    heading: 'Success',
                    text: 'You Like this Post',
                    icon: 'success',
                    position: 'bottom-right'
                })
                
                
            }
        },
        error: function (response) {

            alert("error" + response.message);
        }
    });

}

function AddFav(id) {

    $.ajax({
        type: "POST",
        url: "/Posts/AddFav?id=" + id,

        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.status == true) {
                $.toast({
                    heading: 'Success',
                    text: 'Your Favorite this Post',
                    icon: 'success',
                    position: 'bottom-right'
                })


            }
        },
        error: function (response) {

            alert("error" + response.message);
        }
    });

}