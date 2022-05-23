
export const getData = async address => 

    fetch(address).then(response => {
        //console.log(response);
        return response.json();
      }).then(data => {
        // Work with JSON data here
        //console.log(data);S
        return data;
      }).catch(err => {
        // Do something for an error here
        console.log("Error Reading data " + err);
      });
