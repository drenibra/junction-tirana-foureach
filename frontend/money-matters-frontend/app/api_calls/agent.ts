import axios from "axios";

const api = axios.create({
  baseURL: "http://localhost:5116/api/",
  withCredentials: true,
});

const requests = {
  get: (url: string) => api.get(url).then((res) => res.data),
  post: (url: string, body: any = {}) =>
    api.post(url, body).then((res) => res.data),
  put: (url: string, body: any = {}) =>
    api.put(url, body).then((res) => res.data),
  patch: (url: string, body: any = {}) =>
    api.patch(url, body).then((res) => res.data),
  del: (url: string) => api.delete(url).then((res) => res.data),
};

const Auth = {
  login: (data: any) => requests.post("Auth/login", data),
  register: (data: any) => requests.post("Auth/register", data),
  current: () => requests.get("Auth/me"),
  logout: () => requests.post("Auth/logout"),
};

const Expenses = {
  getByUser: (personId: number) => requests.get(`Expenses/user/${personId}`),
  getSummary: (personId: number) =>
    requests.get(`Expenses/user/${personId}/summary`),
  create: (data: any) => requests.post(`Expenses`, data),
  delete: (id: number, personId: number) =>
    requests.del(`Expenses/${id}/user/${personId}`),
};

const Challenges = {
  getAll: () => requests.get("Challenge"),
  getById: (id: number) => requests.get(`Challenge/${id}`),
  create: (data: any) => requests.post("Challenge", data),
  update: (id: number, data: any) => requests.put(`Challenge/${id}`, data),
  delete: (id: number) => requests.del(`Challenge/${id}`),
};

const Goals = {
  getAll: () => requests.get("Goal"),
  getById: (id: number) => requests.get(`Goal/${id}`),
  create: (data: any) => requests.post("Goal", data),
  delete: (id: number) => requests.del(`Goal/${id}`),
};

const Lessons = {
  getAll: () => requests.get("Lesson"),
  getById: (id: number) => requests.get(`Lesson/${id}`),
  create: (data: any) => requests.post("Lesson", data),
  update: (id: number, data: any) => requests.put(`Lesson/${id}`, data),
  delete: (id: number) => requests.del(`Lesson/${id}`),
};

const Persons = {
  getAll: () => requests.get("Person"),
  getById: (id: number) => requests.get(`Person/${id}`),
  create: (data: any) => requests.post("Person", data),
  update: (id: number, data: any) => requests.put(`Person/${id}`, data),
  delete: (id: number) => requests.del(`Person/${id}`),
};

const agent = {
  Auth,
  Expenses,
  Challenges,
  Goals,
  Lessons,
  Persons,
};

export default agent;
