export enum Permissions
{
    User     = 0,
    Admin    = 0 << 1,
    Employee = 0 << 2,
}

export function hasPermission(source: Permissions, permission: Permissions) : boolean {
    return permission === (source & permission);
}