import appSettings from '../../Settings/Components/TimeRegistration/TimeRegistrationEdit.json';


const { getData } = require('./JSONData');
const address = '../../Settings/Components/TimeRegistration/TimeRegistrationEdit.json';

test(
    `Testintg that the ${getData.name} method returns correct JSON objects`,
    () => {
        getData(address)
        .then( result =>{
            data = result;
            expect(result)
            .toBe(appSettings)
        }
        )
    }
);