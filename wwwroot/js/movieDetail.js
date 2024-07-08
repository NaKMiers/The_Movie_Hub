$(function () {
  const citySelector = $('#citySelector')
  const city = citySelector.val()
  const movieId = $('#MovieId').data('movie-id')
  const day = $('input[name="day"]:checked').data('day')
  console.log('city', city, 'movieId', movieId, 'day', day)

  const getShowTimesTheaters = function (city, movieId, date) {
    console.log({
      City: city,
      MovieId: movieId,
      Date: date,
    })
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
        console.log('theaters', theaters)

        const html = theaters.map(theater => {
          const roomTypeHtml = theater.roomTypeShowTimes.map(roomTypeShowTime => {
            const timeHtml = roomTypeShowTime.showTimes.map(showTime => {
              return `
                <input id="${showTime.id}-${roomTypeShowTime.title}" class="peer" hidden type="radio" name="showTimeId" value="${showTime.id}" />
                <label for="${showTime.id}-${roomTypeShowTime.title}" type="button" class="peer-checked:border-highlight peer-checked:text-highlight rounded-md border border-white px-2 py-1 font-semibold cursor-pointer" onclick="submitFormWithQueryParams()">${showTime.time}</label>
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

        console.log('html', html)
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

  citySelector.change(function () {
    const city = $(this).val()
    getShowTimesTheaters(city, movieId, day)
  })
})
