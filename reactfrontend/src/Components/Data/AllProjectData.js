const appSettings = require('../../Settings/Components/Data/AllProjectData.json');

export const fetchAllProjects = async () => {
  const response = await fetch(appSettings.apiUrl)
  const json = await response.json()

  return  json
}