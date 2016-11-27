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


    /*
    Copy text from any appropriate field to the clipboard
  By Craig Buckler, @craigbuckler
  use it, abuse it, do whatever you like with it!
*/
    (function() {

            'use strict';
  
          // click events
          document.body.addEventListener('click', copy, true);

            // event handler
            function copy(e) {

            // find target element
            var 
              t = e.target,
              c = t.dataset.copytarget,
              inp = (c ? document.querySelector(c) : null);
      
            // is element selectable?
            if (inp && inp.select) {
      
              // select text
              inp.select();

              try {
                // copy text
                document.execCommand('copy');
                inp.blur();
        
                // copied animation
                t.classList.add('copied');
                setTimeout(function() { t.classList.remove('copied'); }, 1500);
              }
              catch (err) {
                alert('please press Ctrl/Cmd+C to copy');
              }
      
            }
    
            }

    })();

     //Click to show checkboxes
    $('#editActiveProfiles').click(function () {
        $('#activeProfilesCB').toggleClass('hidden');
    });
});

