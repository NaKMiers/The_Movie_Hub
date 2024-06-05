// Write your JavaScript code.

$(function () {
   // Float Up on hover
   const hoverFloatUp = $('.hover-float').closest('.relative')

   // add class float-up on hover
   hoverFloatUp.hover(
      function () {
         $(this).find('.hover-float').removeClass('float-down hidden')
         $(this).find('.hover-float').addClass('float-up flex')
      },
      function () {
         $(this).find('.hover-float').removeClass('float-up')
         $(this).find('.hover-float').addClass('float-down')
      }
   )

   // Float Up
   // get all elements has attribute 'float-trigger'
   const floatTrigger = $('[float-trigger]')

   floatTrigger.each(function () {
      const action = $(this).attr('float-trigger')
      const isToggle = $(this).attr('toggle')

      // if toggle is true
      if (isToggle === 'true') {
         $(this).on(action, function () {
            const target = $($(this).attr('target'))
            $(this).toggleClass('active')

            // show
            if (target.hasClass('hidden')) {
               target.removeClass('float-down hidden')
               target.addClass('float-up')
            } else {
               target.removeClass('float-up')
               target.addClass('float-down')

               setTimeout(() => {
                  target.addClass('hidden')
               }, 310)
            }

            // stop propagation for all children of target
            target.children().click(function (e) {
               e.stopPropagation()
            })
         })
         return
      } else {
         $(this).on(action, function () {
            const target = $($(this).attr('target'))

            // show
            target.removeClass('float-down hidden')
            target.addClass('float-up')

            // hide
            target.click(function () {
               $(this).removeClass('float-up')
               $(this).addClass('float-down')
            })

            // stop propagation for all children of target
            target.children().click(function (e) {
               e.stopPropagation()
            })
         })
      }
   })
})
