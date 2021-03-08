import { RequestInit } from '../../typings/fetch.d';

const post = (url: string, init?: RequestInit): Promise<Response> => {
    return fetch(url, { ...init, method: 'post' });
}

const get = (url: string, init?: RequestInit): Promise<Response> => {
    return fetch(url, init);
}

export { post, get }