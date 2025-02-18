export const API_BASE_URL = 'https://localhost:7000';

export const API_ENDPOINTS = {
    Auth: {
        CREATE: `${API_BASE_URL}/User/Create`,
        VALIDATE: `${API_BASE_URL}/User/Validate`,
    },
    User: {
        READ: `${API_BASE_URL}/User/Read`,
        READ_BY_ID: (id: number) => `${API_BASE_URL}/User/Read/${id}`,
        UPDATE: (id: number) => `${API_BASE_URL}/User/UpdateById/${id}`,
        DELETE: (id: number) => `${API_BASE_URL}/User/DeleteById/${id}`,
    },
    Product: {
        CREATE: `${API_BASE_URL}/Product/Create`,
        READ: `${API_BASE_URL}/Product/Read`,
        READ_BY_ID: (id: number) => `${API_BASE_URL}/Product/Read/${id}`,
        UPDATE: (id: number) => `${API_BASE_URL}/Products/UpdateById/${id}`,
        DELETE: (id: number) => `${API_BASE_URL}/products/DeleteById/${id}`,
    },
    Purchase: {
        CREATE: `${API_BASE_URL}/Purchase/Create`,
        READ: `${API_BASE_URL}/Purchase/Read`,
        READ_BY_ID: (id: number) => `${API_BASE_URL}/Purchase/Read/${id}`,
        READ_BY_USER_ID: (id: number) => `${API_BASE_URL}/Purchase/ReadByUserId/user/${id}`,
        UPDATE: (id: number) => `${API_BASE_URL}/Purchase/UpdateById/${id}`,
        DELETE: (id: number) => `${API_BASE_URL}/Purchase/DeleteById/${id}`,
    },
};
