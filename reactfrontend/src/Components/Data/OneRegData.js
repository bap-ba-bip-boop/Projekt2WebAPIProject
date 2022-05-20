const appSettings = require('../../Settings/Components/Data/OneRegData.json');

export const fetchOneReg = async id => {
  const response = await fetch(appSettings.apiUrl+`/${id}`)
  const json = await response.json()

  return  json
}