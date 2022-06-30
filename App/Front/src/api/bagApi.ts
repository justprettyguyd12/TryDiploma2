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

function addBeatToBag(beat: BeatToBag): Promise<void> {
    return fetch(`${baseUrl}`, {
        method: 'PUT',
        headers: {'content-type': 'application/json'},
        body: JSON.stringify(beat)
    }).then();
}

function deleteBeatToBag(beat: BeatToBag): Promise<void> {
    return fetch(`${baseUrl}`, {
        method: 'DELETE',
        headers: {'content-type': 'application/json'},
        body: JSON.stringify(beat)
    }).then();
}

export const bagApi = {
    getBag,
    postBag,
    addBeatToBag,
    deleteBeatToBag
} as const;

export interface Bag{
    Id: Guid;
    ClientId: Guid;
    Beats: Beat[];
}

export interface BeatToBag{
    BeatId: Guid,
    BagID: Guid
}