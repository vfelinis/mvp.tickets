export class ApiRoutesHelper {
    static user = {
        report: '/api/users/report/',
        create: '/api/users/',
        get: (id: number) : string => `/api/users/${id}/`,
        update: (id: number) : string => `/api/users/${id}/`,
        current: '/api/users/current/',
        login: '/api/users/login/',
        logout: '/api/users/logout/',
        
    };

    static category = '/api/categories/';
}