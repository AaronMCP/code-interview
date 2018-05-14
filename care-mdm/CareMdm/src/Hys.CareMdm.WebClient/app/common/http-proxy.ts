import axios from 'axios';
import settings from '../settings';
let instance = axios.create({
  baseURL: settings.apiServer
});

instance.defaults.transformResponse = data => {
  let parsedData = null;
  try {
    const jsonData = JSON.parse(data);
    parsedData = jsonData;
  } catch (e) {
    parsedData = data;
  }

  return parsedData;
};
instance.interceptors.response.use(
  response => {
    // Do something with response data
    return response.data;
  },
  error => {
    if (error.response) {
      return Promise.reject(error.response);
    } else {
      console.log('Error', error.message);
    }
  });

export default instance;
