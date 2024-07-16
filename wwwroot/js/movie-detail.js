$(function () {
  const citySelector = $('#citySelector')
  const city = citySelector.val()
  const movieId = $('#MovieId').data('movie-id')
  const movieTitle = $('#MovieId').data('movie-title')
  const day = $('input[name="day"]:checked').data('day')

  const getShowTimesTheaters = function (city, movieId, date) {
    $.ajax({
      url: '/Api?handler=ShowTimesTheaters',
      type: 'GET',
      data: {
        Date: date,
        MovieId: movieId,
        City: city,
      },
      success: function (data) {
        const { theaters } = data

        const html = theaters.map((theater) => {
          const roomTypeHtml = theater.roomTypeShowTimes.map((roomTypeShowTime) => {
            const timeHtml = roomTypeShowTime.showTimes.map((showTime) => {
              console.log('showTime: ', showTime)

              return `
                <div class="rounded-md border border-white ">
                  <input
                    id="${showTime.id}-${roomTypeShowTime.title}"
                    class="peer" hidden type="radio" name="showTimeId" data-room-id="${
                      roomTypeShowTime.roomId
                    }"
                    data-theater='${JSON.stringify(theater)}'
                    data-show-time='${JSON.stringify(showTime)}'
                    value="${showTime.id}"/>

                    <label
                      for="${showTime.id}-${roomTypeShowTime.title}"
                      type="button"
                      class="px-2 py-1 peer-checked:border-highlight peer-checked:text-highlight font-semibold cursor-pointer">
                      ${showTime.time}
                    </label>
                </div>
              `
            })

            return `
              <h5 class="mb-3 mt-3">${roomTypeShowTime.title}</h5>
              <div class="flex flex-wrap gap-2">
                ${timeHtml.join('')}
              </div>
            `
          })

          return `
          <div class="bg-primary-tw rounded-lg shadow-lg p-6">
              <h4 class="font-semibold text-highlight text-2xl">${theater.name}</h4>
              <p class="my-6">${theater.address}</p>

              ${roomTypeHtml.join('')}
          </div>
        `
        })

        $('#theaterContainer').html(html)
      },
      error: function (xhr, status, error) {
        console.error('Error', xhr, status, error)
      },
    })
  }

  if (city && movieId && day) {
    getShowTimesTheaters(city, movieId, day)
  }

  // day selector
  $('input[name="day"]').change(function () {
    const day = $(this).data('day')
    getShowTimesTheaters(city, movieId, day)
  })

  // city selector
  citySelector.change(function () {
    const city = $(this).val()
    getShowTimesTheaters(city, movieId, day)
  })

  // selected show time
  let selectedRoom = null
  let selectedTheater = null
  let selectedShowTime = null
  $(document).on('change', 'input[name="showTimeId"]', function () {
    selectedTheater = $(this).data('theater')
    selectedShowTime = $(this).data('show-time')

    const roomId = $(this).data('room-id')

    $.ajax({
      url: '/Api?handler=TicketTypes',
      type: 'GET',
      data: {
        RoomId: roomId,
      },
      success: function (data) {
        const { ticketTypes, seats, room } = data
        selectedRoom = room

        $('#selected-room').text(room.name)

        const html = ticketTypes.map((ticketType) => {
          const formattedPrice = ticketType.price.toLocaleString('vi-VN', {
            style: 'currency',
            currency: 'VND',
          })

          return `
            <div class="flex flex-col gap-2 border border-slate-300 rounded-lg p-5 uppercase font-semibold">
              <p>${ticketType.label}</p>
              <p>${formattedPrice}</p>

              <input data-ticket-type='${JSON.stringify(
                ticketType
              )}' name="ticket-type-quantity" type="number" min="0" value="0"
                  class="w-16 h-10 outline-none text-center text-xl appearance-none rounded-lg shadow-lg text-dark"/  >
          </div>
          `
        })

        // show total bar
        $('#total-bar').removeClass('hidden')
        $('#total-bar').addClass('flex')
        $('#total-bar').find('.selected-theater-name').text(selectedTheater.name)

        // show choose ticket and seat
        $('section#ChooseTicket').removeClass('hidden')
        $('#ticketTypeContainer').html(html)

        // show choose seat
        $('section#ChooseSeats').removeClass('hidden')

        // calculate max row and column
        const maxRow = Math.max(...seats.map((seat) => seat.seatRow))
        const maxColumn = Math.max(...seats.map((seat) => seat.seatColumn))

        console.log('maxRow: ', maxRow)
        console.log('maxColumn: ', maxColumn)

        // create a matrix of seats
        let seatMatrix = Array.from({ length: maxRow + 1 }, () => Array(maxColumn + 1).fill(null))

        seats.forEach((seat) => {
          seatMatrix[seat.seatRow][seat.seatColumn] = seat
        })

        seatMatrix = seatMatrix.slice(1).map((row) => row.slice(1))

        // render seat matrix
        const seatMatrixHtml = seatMatrix.map((row, rowIndex) => {
          const colHtml = row.map((seat, colIndex) => {
            return !seat
              ? `<div class="w-12"></div>`
              : `
              <div>
                <input ${seat.isAvailable ? '' : 'disabled'} id="${seat.seatRow}-${
                  seat.seatColumn
                }" hidden type="checkbox" data-seat-id="${seat.id}" data-seat-type="${
                  seat.seatType
                }" name="seat" class="peer"/>
                <label for="${seat.seatRow}-${
                  seat.seatColumn
                }" class="peer-checked:text-dark peer-checked:bg-highlight peer-disabled:pointer-events-none cursor-pointer h-10 w-12 flex items-center justify-center text-sm font-semibold rounded-lg border-2 ${
                  seat.seatType == 'normal'
                    ? 'border-slate-300'
                    : seat.seatType == 'vip'
                    ? 'border-green-500'
                    : 'border-rose-500'
                } ${seat.isAvailable ? '' : 'bg-slate-300'}">${seat.seatRow}-${seat.seatColumn}</label>
              </div>
            `
          })

          return `
            <div class="flex gap-2">
              ${colHtml.join('')}
            </div>
          `
        })

        $('#seatContainer').html(seatMatrixHtml)
      },
      error: function (xhr, status, error) {
        console.error('Error', xhr, status, error)
      },
    })
  })

  // change ticket type quantity
  const ticketTypeQuantities = {}
  let totalPrice = 0
  $(document).on('change', 'input[name="ticket-type-quantity"]', function () {
    const quantity = +$(this).val()
    const ticketTypeData = $(this).data('ticket-type')
    console.log('ticketTypeData: ', ticketTypeData)
    ticketTypeQuantities[ticketTypeData.label] = {
      id: ticketTypeData.id,
      price: ticketTypeData.price,
      quantity,
    }

    const ticketTypeString = Object.entries(ticketTypeQuantities)
      .map(([label, { quantity }]) => `${quantity} ${label}`)
      .join(', ')
    const selectedTheaterName = $('#total-bar').find('.selected-theater-name')
    selectedTheaterName.text(selectedTheater.name + ' | ' + ticketTypeString)

    console.log(ticketTypeQuantities)

    // calc total price
    totalPrice = 0
    for (const key in ticketTypeQuantities) {
      let subPrice = 0
      if (ticketTypeQuantities.hasOwnProperty(key)) {
        console.log('ticketTypeQuantities[key]: ', ticketTypeQuantities[key])
        const { price, quantity } = ticketTypeQuantities[key]
        subPrice = price * quantity
      }

      totalPrice += subPrice
    }

    $('#total-price').text(totalPrice.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' }))
  })

  // function to validate ticket quantity
  function totalQuantity() {
    let totalQuantity = 0
    for (const key in ticketTypeQuantities) {
      if (ticketTypeQuantities.hasOwnProperty(key)) {
        totalQuantity += ticketTypeQuantities[key].quantity
      }
    }
    return totalQuantity
  }

  // function to check if the new seat is adjacent to the already selected seats
  function isAdjacent(newSeat) {
    if (selectedSeats.length === 0) return true

    return selectedSeats.some((seat) => {
      const rowDiff = Math.abs(seat.row - newSeat.row)
      const colDiff = Math.abs(seat.column - newSeat.column)
      return (rowDiff === 0 && colDiff === 1) || (rowDiff === 1 && colDiff === 0)
    })
  }

  const selectedSeats = []
  $(document).on('change', 'input[name="seat"]', function () {
    const seatRow = $(this).attr('id').split('-')[0]
    const seatColumn = $(this).attr('id').split('-')[1]
    const seatType = $(this).data('seat-type')
    const seatId = $(this).data('seat-id')

    if ($(this).is(':checked')) {
      if (selectedSeats.length >= totalQuantity()) {
        alert('Số lượng không hợp lệ. Hãy điều chỉnh lại số lượng vé.')
        $(this).prop('checked', false)
        return
      }

      const newSeat = { id: seatId, row: seatRow, column: seatColumn, seatType }
      if (!isAdjacent(newSeat)) {
        alert('Các ghế phải kế nhau.')
        $(this).prop('checked', false)
        return
      }

      selectedSeats.push(newSeat)
    } else {
      const index = selectedSeats.findIndex((seat) => seat.row === seatRow && seat.column === seatColumn)
      if (index !== -1) {
        selectedSeats.splice(index, 1)
      }
    }

    console.log('selectedShowTime: ', selectedShowTime)

    if (selectedSeats.length > 0) {
      // show info
      const selectedRoomSeatTime = $('#total-bar').find('.selected-room-seat-time')
      let str = `Phòng chiếu: ${selectedRoom.name} | ${selectedSeats
        .map((s) => s.row + '-' + s.column)
        .join(', ')}`

      selectedRoomSeatTime.text(str)
    }
  })

  // store data to local storage when click on order-ticket-btn
  $('#order-ticket-btn').click(function () {
    if (selectedSeats.length < totalQuantity()) {
      alert('Vui lòng chọn đủ số lượng ghế.')
      return
    }

    const ticket = {
      movieId,
      movieTitle,
      theaterId: selectedTheater.id,
      theater: selectedTheater,
      roomId: selectedRoom.id,
      room: selectedRoom,
      showTimeId: selectedShowTime.id,
      showTime: selectedShowTime,
      ticketTypes: Object.entries(ticketTypeQuantities).map(([label, { id, price, quantity }]) => ({
        id,
        label,
        price,
        quantity,
      })),
      seats: selectedSeats,
      startAt: selectedShowTime.time,
      total: totalPrice,
    }

    localStorage.setItem('ticket', JSON.stringify(ticket))

    // redirect to checkout page
    window.location.href = '/Checkout'
  })
})
