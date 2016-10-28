jQuery(function ($) {

    /* ----------------------------------------------------------- */
    /*  SCROLL BUTTONS
    /* ----------------------------------------------------------- */

    //Check to see if the window is top if not then display button
    $(window).scroll(function () {
        if ($(this).scrollTop() > 300) {
            $('#scrollTop').fadeIn();
        } else {
            $('#scrollTop').fadeOut();
        }
    });

    //Click event to scroll to top
    $('#scrollTop').click(function () {
        $('html, body').animate({ scrollTop: 0 }, 800);
        return false;
    });

    	/* ----------------------------------------------------------- */
	/*  USER MENU NAV OPEN  
	/* ----------------------------------------------------------- */ 
               $(document).ready(function () {
        var isShowing = false;

        var toggleMenu = function() {
            isShowing = !isShowing; //change boolean to reflect state change
            $('.navclass_user').toggleClass('showIt').addClass('animated fadeInDown');; //show or hide element
        }

        $(document).on('click', function (e) {
            if($(e.target).is('.dropdown-toggle') || $(".dropdown-toggle").has(e.target).length !== 0) {
                toggleMenu();
            } else {
                isShowing = false;
                $('.navclass_user').removeClass('showIt');  
            }
        });
               });

        	$(document).ready(function(){
						$('.usertoggle').hover(function() {
							$('.navclass_user').addClass('showIt').addClass('animated fadeInDown');
						})
				    }); 	
    
    	/* ----------------------------------------------------------- */
	/*  SOCIAL MENU NAV OPEN  
	/* ----------------------------------------------------------- */
    $(document).ready(function () {
        var isShowing = false;

        var toggleMenu = function() {
            isShowing = !isShowing; //change boolean to reflect state change
            $('.social_nav_class').toggleClass('showIt').addClass('animated fadeInDown');;
            $("#socialnav").removeClass('animated').toggleClass('showIt');//show or hide element
        }

        $(document).on('click', function (e) {
            if($(e.target).is('.menu-trigger') || $(".menu-trigger").has(e.target).length !== 0) {
                toggleMenu();
            } else {
                isShowing = false;
                $('.social_nav_class').removeClass('showIt');  
                $("#socialnav").removeClass('showIt');
            }
        });
    });
});