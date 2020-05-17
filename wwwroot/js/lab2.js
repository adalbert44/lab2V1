const uri = 'api/Kingdoms';
let kingdoms = [];
function getKingdoms() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayKingdoms(data))
    .catch(error => console.error('Unable to get kingdoms.', error));
}
function addKingdom() {
    const addNameTextbox = document.getElementById('add-name');
    const addInfoTextbox = document.getElementById('add-info');
    const kingdom = {
        name: addNameTextbox.value.trim(),
        info: addInfoTextbox.value.trim(),
    };
    fetch(uri, {
        method: 'POST',
    headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
    },
    body: JSON.stringify(kingdom)
    })
        .then(response => response.json())
        .then(() => {
            getKingdoms();
            addNameTextbox.value = '';
            addInfoTextbox.value = '';
        })
        .catch (error => console.error('Unable to add kingdom.', error));
}
function deleteKingdom(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getKingdoms())
        .catch (error => console.error('Unable to delete kingdom.', error));
}
function displayEditForm(id) {
    const kingdom = kingdoms.find(kingdom => kingdom.id === id);
    document.getElementById('edit-id').value = kingdom.id;
    document.getElementById('edit-name').value = kingdom.name;
    document.getElementById('edit-info').value = kingdom.info;
    document.getElementById('editForm').style.display = 'block';
}
function updateKingdom() {
    const kingdomId = document.getElementById('edit-id').value;
    const kingdom = {
        id: parseInt(kingdomId, 10),
        name: document.getElementById('edit-name').value.trim(),
        info: document.getElementById('edit-info').value.trim()
    };
    fetch(`${uri}/${kingdomId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(kingdom)
    })
        .then(() => getKingdoms())
        .catch (error => console.error('Unable to update kingdom.', error));
    closeInput();
    return false;
}
function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}
function _displayKingdoms(data) {
    const tBody = document.getElementById('kingdoms');
    tBody.innerHTML = '';

    const button = document.createElement('button');
    data.forEach(kingdom => {
        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${kingdom.id})`);
        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteKingdom(${kingdom.id})`);
        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let textNode = document.createTextNode(kingdom.name);
        td1.appendChild(textNode);
        let td2 = tr.insertCell(1);
        let textNodeInfo = document.createTextNode(kingdom.info);
        td2.appendChild(textNodeInfo);
        let td3 = tr.insertCell(2);
        td3.appendChild(editButton);
        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);
    });
    kingdoms = data;
}