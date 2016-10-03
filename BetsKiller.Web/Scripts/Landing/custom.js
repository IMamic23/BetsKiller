/**	
	* SinglePro HTML 2.0	
	* Template Scripts
	* Created by WpFreeware Team

	Custom JS
	
	1. Superslides Slider
	2. Fixed Top Menubar
	3. Featured Slider
	4. Wow animation
	5. SCROLL BUTTONS
	6. MOBILE MENU CLOSE 	
	
**/

jQuery(function($){

	/* ----------------------------------------------------------- */
	/*  1. Superslides Slider
	/* ----------------------------------------------------------- */
	jQuery('#slides').superslides({
      animation: 'slide',
      play: '9000'
    });
	
	
	/* ----------------------------------------------------------- */
	/*  2. Fixed Top Menubar
	/* ----------------------------------------------------------- */

	// For fixed top bar
    $(window).scroll(function(){
        if($(window).scrollTop() >100 /*or $(window).height()*/){
            $(".navbar-fixed-top").addClass('past-main');   
        }
    else{    	
      $(".navbar-fixed-top").removeClass('past-main');
      }
    });


	/* ----------------------------------------------------------- */
	/*  3. Featured Slider
	/* ----------------------------------------------------------- */

    $('.featured_slider').slick({
      dots: true,
      infinite: true,      
      speed: 800,
      arrows:false,      
      slidesToShow: 1,
      slide: 'div',
      autoplay: true,
      fade: true,
      autoplaySpeed: 5000,
      cssEase: 'linear'
    });

	
	/* ----------------------------------------------------------- */
	/*  4. Wow smooth animation
	/* ----------------------------------------------------------- */

	wow = new WOW(
      {
        animateClass: 'animated',
        offset:       100
      }
    );
    wow.init();
	

	/* ----------------------------------------------------------- */
	/*  5. SCROLL BUTTONS
	/* ----------------------------------------------------------- */

    $('#scrollBottom').fadeIn();

	//Check to see if the window is top if not then display button
	$(window).scroll(function(){
		if ($(this).scrollTop() > 300) {
		  $('#scrollTop').fadeIn();
		  $('#scrollBottom').fadeOut();
		} else {
		  $('#scrollTop').fadeOut();
		  $('#scrollBottom').fadeIn();
		}
	});
	 
	//Click event to scroll to top
	$('#scrollTop').click(function(){
		$('html, body').animate({scrollTop : 0},800);
		return false;
	});
	
	//Click event to scroll bottom
	$('#scrollBottom').click(function(){
		$('html, body').animate({scrollTop: $('#service').offset().top},800);
		return false;
	});


	/* ----------------------------------------------------------- */
	/*  6. MOBILE MENU ACTIVE CLOSE 
	/* ----------------------------------------------------------- */ 

	$('.navbar-nav').on('click', 'li a', function() {
	  $('.navbar-collapse').collapse('hide');
	});

    	/* ----------------------------------------------------------- */
	/*  7. SOCIAL MENU NAV OPEN  
	/* ----------------------------------------------------------- */ 
      
       
              
});
