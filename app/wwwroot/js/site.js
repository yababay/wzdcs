// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//

function testWZD(e){
    e = e || window.event
    var show = e.target.parentNode.parentNode.querySelector('code')
    var url = '/wzdcs/' + e.target.id.replace(/-/, '').replace(/-/, '/') 
    var options = {method: 'GET'}
    if(/.*post$/.test(url)){
        var body = new FormData()
        body.append('content', JSON.stringify({'content': 'У попа была собака, Он ее любил.'}))
        options = {
            method: 'POST',
            body
        }
    }
    if(/.*binary$/.test(url)){
        var form = e.target.parentNode.parentNode.querySelector('form')
        var body = new FormData(form)
        options = {
            method: 'POST',
            body
        }
    }
    fetch(url, options)
        .then(res => res.text())
        .then(txt => {
            console.log(txt)
            show.textContent = txt
        })
}
