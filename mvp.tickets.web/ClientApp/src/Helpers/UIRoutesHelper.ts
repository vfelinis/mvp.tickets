export class UIRoutesHelper {
    static home: IRoute = {
        path: '/',
        getRoute: () => `${this.home.path}`,
    };
    static tickets: IRoute = {
        path: 'tickets',
        getRoute: () => `/${this.tickets.path}/`,
    };
    static ticketsCreate: IRoute = {
        path: 'tickets/create',
        getRoute: () => `/${this.ticketsCreate.path}/`,
    };
    static ticketsUpdate: IRoute = {
        path: 'tickets/:id',
        getRoute: (id: number) => `/${this.ticketsUpdate.path.replace(":id", id.toString())}/`,
    };
    static adminUsers: IRoute = {
        path: 'admin/users',
        getRoute: () => `/${this.adminUsers.path}/`,
    };
    static adminUsersCreate: IRoute = {
        path: 'admin/users/create',
        getRoute: () => `/${this.adminUsersCreate.path}/`,
    };
    static adminUsersUpdate: IRoute = {
        path: 'admin/users/:id',
        getRoute: (id: number) => `/${this.adminUsersUpdate.path.replace(":id", id.toString())}/`,
    };
    static adminCategories: IRoute = {
        path: 'admin/categories',
        getRoute: () => `/${this.adminCategories.path}/`,
    };
    static adminCategoriesCreate: IRoute = {
        path: 'admin/categories/create',
        getRoute: () => `/${this.adminCategoriesCreate.path}/`,
    };
    static adminCategoriesUpdate: IRoute = {
        path: 'admin/categories/:id',
        getRoute: (id: number) => `/${this.adminCategoriesUpdate.path.replace(":id", id.toString())}/`,
    };
    static adminPriorities: IRoute = {
        path: 'admin/priorities',
        getRoute: () => `/${this.adminPriorities.path}/`,
    };
    static adminPrioritiesCreate: IRoute = {
        path: 'admin/priorities/create',
        getRoute: () => `/${this.adminPrioritiesCreate.path}/`,
    };
    static adminPrioritiesUpdate: IRoute = {
        path: 'admin/priorities/:id',
        getRoute: (id: number) => `/${this.adminPrioritiesUpdate.path.replace(":id", id.toString())}/`,
    };
    static adminQueues: IRoute = {
        path: 'admin/queues',
        getRoute: () => `/${this.adminQueues.path}/`,
    };
    static adminQueuesCreate: IRoute = {
        path: 'admin/queues/create',
        getRoute: () => `/${this.adminQueuesCreate.path}/`,
    };
    static adminQueuesUpdate: IRoute = {
        path: 'admin/queues/:id',
        getRoute: (id: number) => `/${this.adminQueuesUpdate.path.replace(":id", id.toString())}/`,
    };
    static adminStatusRules: IRoute = {
        path: 'admin/statusrules',
        getRoute: () => `/${this.adminStatusRules.path}/`,
    };
    static adminStatusRulesCreate: IRoute = {
        path: 'admin/statusrules/create',
        getRoute: () => `/${this.adminStatusRulesCreate.path}/`,
    };
    static adminStatusRulesUpdate: IRoute = {
        path: 'admin/statusrules/:id',
        getRoute: (id: number) => `/${this.adminStatusRulesUpdate.path.replace(":id", id.toString())}/`,
    };
    static adminStatuses: IRoute = {
        path: 'admin/statuses',
        getRoute: () => `/${this.adminStatuses.path}/`,
    };
    static adminStatusesCreate: IRoute = {
        path: 'admin/statuses/create',
        getRoute: () => `/${this.adminStatusesCreate.path}/`,
    };
    static adminStatusesUpdate: IRoute = {
        path: 'admin/statuses/:id',
        getRoute: (id: number) => `/${this.adminStatusesUpdate.path.replace(":id", id.toString())}/`,
    };
    static adminResolutions: IRoute = {
        path: 'admin/resolutions',
        getRoute: () => `/${this.adminResolutions.path}/`,
    };
    static adminResolutionsCreate: IRoute = {
        path: 'admin/resolutions/create',
        getRoute: () => `/${this.adminResolutionsCreate.path}/`,
    };
    static adminResolutionsUpdate: IRoute = {
        path: 'admin/resolutions/:id',
        getRoute: (id: number) => `/${this.adminResolutionsUpdate.path.replace(":id", id.toString())}/`,
    };
    static adminTemplateTypes: IRoute = {
        path: 'admin/templatetypes',
        getRoute: () => `/${this.adminTemplateTypes.path}/`,
    };
    static adminTemplateTypesCreate: IRoute = {
        path: 'admin/templatetypes/create',
        getRoute: () => `/${this.adminTemplateTypesCreate.path}/`,
    };
    static adminTemplateTypesUpdate: IRoute = {
        path: 'admin/templatetypes/:id',
        getRoute: (id: number) => `/${this.adminTemplateTypesUpdate.path.replace(":id", id.toString())}/`,
    };
    static adminTemplates: IRoute = {
        path: 'admin/templates',
        getRoute: () => `/${this.adminTemplates.path}/`,
    };
    static adminTemplatesCreate: IRoute = {
        path: 'admin/templates/create',
        getRoute: () => `/${this.adminTemplatesCreate.path}/`,
    };
    static adminTemplatesUpdate: IRoute = {
        path: 'admin/templates/:id',
        getRoute: (id: number) => `/${this.adminTemplatesUpdate.path.replace(":id", id.toString())}/`,
    };
    static employee: IRoute = {
        path: 'employee',
        getRoute: () => `/${this.employee.path}/`,
    };
    static login: IRoute = {
        path: 'login',
        getRoute: () => `/${this.login.path}/`,
    };
    static notFound: IRoute = {
        path: '404',
        getRoute: () => `/${this.notFound.path}/`,
    };
}

export interface IRoute {
    path: string,
    getRoute: Function
}