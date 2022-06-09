export enum Permissions
{
    User     = 0,
    Admin    = 1 << 0,
    Employee = 1 << 1,
}

export function hasPermission(source: Permissions, permission: Permissions) : boolean {
    return permission === (source & permission);
}