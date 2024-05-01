document.addEventListener('DOMContentLoaded', function () {
    handleProfileImageUpload()
    select()

    let sw = document.querySelector('#switch-mode')
    sw.addEventListener('change', function () {
        let theme = this.checked ? "dark" : "light"
        fetch(`/settings/changetheme?theme=${theme}`)
            .then(res => {
                if (res.ok)
                    window.location.reload()
                else 
                    console.log('Something went wrong with changing theme.')
            })
    })
})

function handleProfileImageUpload() {
    try {
        let fileUploader = document.querySelector('#fileUploader')
        if (fileUploader != undefined) {
            fileUploader.addEventListener('change', function () {
                if (this.files.length > 0) {
                    this.form.submit()
                }
            })
        }
    }
    catch { }
}

const toggleMenu = () => {
    console.log("Button clicked!");
    document.getElementById('mobile-menu').classList.toggle('hide');
    document.getElementById('account-buttons').classList.toggle('hide');
       
}

const checkScreenSize = () => {
    if (window.innerWidth >= 1200) {
        document.getElementById('menu').classList.remove('hide');
        document.getElementById('account-buttons').classList.remove('hide');
    } else {
        if (!document.getElementById('menu').classList.contains('hide')) {
            document.getElementById('menu').classList.add('hide');
        }
        if (!document.getElementById('account-buttons').classList.contains('hide')) {
            document.getElementById('account-buttons').classList.add('hide');
        }
    }
};

function select() {
    try {
        let select = document.querySelector('.select')
        let selected = document.querySelector('.selected')
        let selectOptions = select.querySelector('.select-options')

        selected.addEventListener('click', function () {
            selectOptions.style.display = (selectOptions.style.display == 'block') ? 'none' : 'block'
        })

        let options = selectOptions.querySelectorAll('.option')
        options.forEach(function (option) {
            option.addEventListener('click', function () {
                selected.innerHTML = this.textContent
                selectOptions.style.display = 'none'
                let category = this.getAttribute('data-value')
                selected.setAttribute('data-value', category)
                updateCoursesByFilter()
                searchQuery()
            })
        })
    }
    catch { }
}

function searchQuery() { 

    try {
        document.querySelector('#searchQuery').addEventListener('keyup', function () {
            updateCoursesByFilter()
        })
    }
    catch { }
}

function updateCoursesByFilter() {
    const category = document.querySelector('.select .selected').getAttribute('data-value') || 'all'
    const searchQuery = document.querySelector('#searchQuery').getAttribute('data-value') || 'all'

    console.log(category)
    const url = `/courses/index?category=${encodeURIComponent(category)}&serchQuery=${encodeURIComponent(searchQuery)}`

    fetch(url)
        .then(res => res.text())
        .then(data => {
            const parser = new DOMParser()
            const dom = parser.parseFromString(data, 'text/html')
            document.querySelector('.items').innerHTML = dom.querySelector('.items').innerHTML
        })
}

window.addEventListener('resize', checkScreenSize);
checkScreenSize();