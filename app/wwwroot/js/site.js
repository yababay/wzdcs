// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//

function testWZD(e){
    e = e || window.event
    var show = e.target.parentNode.parentNode.querySelector('code')
    var url = '/wzdcs/' + e.target.id.replace('-', '/') 
    fetch(url)
        .then(res => res.text())
        .then(txt => {
            console.log(txt)
            show.textContent = txt
        })
}
