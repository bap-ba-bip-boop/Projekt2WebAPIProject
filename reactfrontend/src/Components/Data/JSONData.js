
export const getData = async address => 

    fetch(address).then(response => {
        return response.json();
      }).then(data => {
        return data;
      }).catch(err => {
        console.log("Error Reading data " + err);
      });
