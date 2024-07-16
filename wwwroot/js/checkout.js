$(function () {
  // LEFT
  let curStep = 1

  const prevStepBtn = $('#prev-step-btn')
  const nextStepBtn = $('#next-step-btn')

  let selectedPaymentMethod = ''
  const paymentMethods = $('input[name="payment-method"]')
  paymentMethods.change(function () {
    selectedPaymentMethod = $(this).data('value')
    console.log('selectedPaymentMethod', selectedPaymentMethod)
    updateStep()
  })

  const checkSelectedPaymentMethod = () => {
    const paymentMethod = $('input[name="payment-method"]:checked').val()
    return !!paymentMethod
  }

  const updateStep = () => {
    const steps = $('.checkout__step')
    const showSteps = $('.checkout__show-step')

    steps.each(function () {
      const step = $(this)
      step.addClass('hidden')
      step.removeClass('flex')
    })

    showSteps.each(function () {
      const showStep = $(this)
      showStep.removeClass('text-highlight')
    })

    showSteps.eq(curStep - 1).addClass('text-highlight')

    steps.eq(curStep - 1).removeClass('hidden')
    steps.eq(curStep - 1).addClass('flex')

    // change text of next button
    if (curStep === 2) {
      if (!checkSelectedPaymentMethod()) {
        nextStepBtn.disabled = true
        nextStepBtn.removeClass('bg-highlight')
        nextStepBtn.addClass('pointer-events-none bg-gray-300')
      } else {
        nextStepBtn.disabled = false
        nextStepBtn.removeClass('pointer-events-none bg-gray-300')
        nextStepBtn.addClass('bg-highlight')
      }

      nextStepBtn.html(
        `<span class='uppercase font-body text-sm tracking-widest group-hover:text-light trans-300 z-10'>
          Thanh Toán
        </span>`
      )
    } else {
      nextStepBtn.html(
        `<span class='uppercase font-body text-sm tracking-widest group-hover:text-light trans-300 z-10'>
          Tiếp Tục
        </span>`
      )
    }
  }
  updateStep()

  const nextStep = () => {
    if (curStep < 3) {
      curStep++
      updateStep()
    }
  }

  const prevStep = () => {
    if (curStep > 1) {
      curStep--
      updateStep()
    }
  }

  const validateForm1 = () => {
    // validate form 1
    const step1Form = $('#step-1-form')
    const fullName = step1Form.find('input[name="fullName"]').val().trim()
    const phone = step1Form.find('input[name="phone"]').val().trim()
    const email = step1Form.find('input[name="email"]').val().trim()

    if (!fullName) {
      alert('Hãy nhập tên')
      return false
    }

    if (!phone) {
      alert('Hãy nhập số điện thoại')
      return false
    }

    if (!email) {
      alert('Hãy nhập email')
      return false
    }

    return true
  }

  nextStepBtn.click(function () {
    if (curStep === 1) {
      if (validateForm1()) {
        nextStep()
      }
    }

    if (curStep === 2) {
      if (checkSelectedPaymentMethod()) {
        handleCheckout()
      }
    }
  })

  prevStepBtn.click(function () {
    prevStep()
  })

  const handleCheckout = () => {
    const ticketData = JSON.parse(localStorage.getItem('ticket'))

    if (!ticketData) {
      window.location.href = '/'
    }

    const step1Form = $('#step-1-form')
    const fullName = step1Form.find('input[name="fullName"]').val().trim()
    const phone = step1Form.find('input[name="phone"]').val().trim()
    const email = step1Form.find('input[name="email"]').val().trim()

    const data = {
      ...ticketData,
      fullName,
      phone,
      email,
      ticketTypes: JSON.stringify(ticketData.ticketTypes),
      seats: JSON.stringify(ticketData.seats),
      showTime: JSON.stringify(ticketData.showTime),
      paymentMethod: selectedPaymentMethod,
    }

    $.ajax({
      url: '/Checkout?handler=Checkout',
      type: 'GET',
      data: data,
      success: function (data) {
        // show success message
        alert('Đặt vé thành công')

        // remove ticket from local storage
        localStorage.removeItem('ticket')

        window.location.href = '/'
      },
      error: function (xhr, status, error) {
        console.error('Error', xhr, status, error)
      },
    })
  }

  // RIGHT
  const ticketData = JSON.parse(localStorage.getItem('ticket'))
  if (!ticketData) {
    window.location.href = '/'
  }

  const movieTitle = $('#checkout__movie-title')
  movieTitle.text(ticketData.movieTitle)

  const theaterName = $('#checkout__theater-name')
  theaterName.text(ticketData.theater.name)

  const theaterAddress = $('#checkout__theater-address')
  theaterAddress.text(ticketData.theater.address)

  const checkoutTime = $('#checkout__time')
  checkoutTime.text(
    ticketData.showTime.date.split('T')[0].split('-').join('/') + ' ' + ticketData.showTime.time
  )

  const checkoutRoom = $('#checkout__room')
  checkoutRoom.text(ticketData.room.name)

  const checkoutTicketType = $('#checkout__ticket-type')
  checkoutTicketType.text(
    ticketData.ticketTypes.map((type) => `${type.label} (${type.quantity})`).join(', ')
  )

  const checkoutSeatType = $('#checkout__seat-type')
  // group by seat type
  const groupSeats = ticketData.seats.reduce((acc, seat) => {
    if (!acc[seat.seatType]) {
      acc[seat.seatType] = []
    }
    acc[seat.seatType].push(({ row, column } = seat))
    return acc
  }, {})

  const seatTypeHtml = Object.entries(groupSeats).map(
    ([seatType, seats]) => `
      <div class="flex gap-6">
        <div class="flex flex-col">
          <span class="text-highlight">Loại ghế</span>
          <span class="text-[20px]">${
            seatType === 'normal' ? 'Ghế thường' : seatType === 'vip' ? 'Ghế VIP' : 'Ghê đôi'
          }</span>
        </div>
        <div class="flex flex-col">
          <span class="text-highlight">Số ghế</span>
          <span class="text-[20px]">${seats
            .map((seat) => `${seat.row}-${seat.column}`)
            .join(', ')}</span>
        </div>
      </div>`
  )
  checkoutSeatType.html(seatTypeHtml)

  const checkoutTotal = $('#checkout__total')
  checkoutTotal.text(
    ticketData.total.toLocaleString('vi-VN', {
      style: 'currency',
      currency: 'VND',
    })
  )
})
