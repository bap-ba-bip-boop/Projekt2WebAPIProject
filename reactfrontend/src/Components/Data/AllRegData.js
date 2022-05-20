const appSettings = require('../../Settings/Components/Data/AllRegData.json')

export const fetchAllRegs = async () => {
  const response = await fetch(appSettings.apiUrl)
  const json = await response.json()

  return  json
}