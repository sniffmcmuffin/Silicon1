document.addEventListener('DOMContentLoaded', function() {
    handleProfileImageUpload()

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

window.addEventListener('resize', checkScreenSize);
checkScreenSize();