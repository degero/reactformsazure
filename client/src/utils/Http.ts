import { RequestInit } from '../../typings/fetch.d';

const post = (url: string, init?: RequestInit): Promise<Response> => {
    return fetch(url, {
        ...init, method: 'post', headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
    });
}

const get = (url: string, init?: RequestInit): Promise<Response> => {
    return fetch(url, init);
}

export { post, get }