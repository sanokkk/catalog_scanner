//document.querySelector('#fetchBtn').addEventListener('click', onClck, false);
document.querySelector('#fetchBtn').addEventListener('click', (event) => {
    event.preventDefault();
    var fr = fetching();
    console.log(fr)
    if (fr){
        linkAdd();
        return;
    }
    else {
        alert('error');
    }
    
})

function fetching() {
    resp = fetch('http://localhost:5000/file').then((response) => console.log(response.ok))
    if (resp.ok){
        return true;
    }
    return false;
}

function linkAdd() {
    var myDiv = document.querySelector('#myDiv');
            var lnk = document.querySelector('#link');
            if (!myDiv.contains(lnk)){
                var link = document.createElement('a');
                link.id = 'link'
                link.href = "127.0.0.1:5500/client/scanner.html";
                link.innerHTML = "Перейти";
                myDiv.appendChild(link);
            }
}




