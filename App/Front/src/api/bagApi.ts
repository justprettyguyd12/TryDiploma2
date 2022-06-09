import {Beat} from "./beatsApi";

const baseUrl = '/bag';

function getBag(clientId: Guid): Promise<Bag> {
    return fetch(`${baseUrl}/${clientId}`).then(x => x.json());
}

function postBag(bag: Bag): Promise<void> {
    return fetch(`${baseUrl}`, {
        method: 'POST',
        headers: {'content-type': 'application/json'},
        body: JSON.stringify(bag)
    }).then();
}

//TODO пересмотреть контроллер корзины, доделать API

export interface Bag{
    Id: Guid;
    ClientId: Guid;
    Beats: Beat[];
}