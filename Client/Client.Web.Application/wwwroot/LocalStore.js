export function AddToStore(key, value) {
    localStorage.setItem(key, value);
}

export function GetFromStore(key) {
    return localStorage.getItem(key);
}

export function DeleteFromStore(key) {
    localStorage.setItem(key, null);
}