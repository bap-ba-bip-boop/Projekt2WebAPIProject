const url = 'https://localhost:7045/project';

export const fetchProjects = async () => {
  const response = await fetch(url)
  const json = await response.json()

  return  json
}