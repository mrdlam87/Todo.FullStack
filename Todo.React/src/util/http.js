import axios from "axios";

const BACKEND_API_URL = "api";

export const fetchUsers = async () => {
  const reponse = await axios.get(BACKEND_API_URL + "/users");

  return reponse.data;
};

export const postUser = async (userData) => {
  const response = await axios.post(BACKEND_API_URL + "/users", userData);

  return response.data.id;
};

export const putUser = async (id, userData) =>
  axios.put(BACKEND_API_URL + `/users/${id}`, userData);

export const delUser = async (id) =>
  axios.delete(BACKEND_API_URL + `/users/${id}`);

export const fetchUserTodos = async (id) => {
  const response = await axios.get(BACKEND_API_URL + `/users/${id}/todos`);

  return response.data;
};

export const postTodo = async (id, todoData) => {
  const response = await axios.post(
    BACKEND_API_URL + `/users/${id}/todos`,
    todoData
  );

  return response.data.id;
};

export const putTodo = async (userId, todoData) =>
  await axios.put(
    BACKEND_API_URL + `/users/${userId}/todos/${todoData.id}`,
    todoData
  );

export const delTodo = async (userId, todoId) =>
  await axios.delete(BACKEND_API_URL + `/users/${userId}/todos/${todoId}`);
