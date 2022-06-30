import {Beat} from "./beatsApi";

const baseUrl = '/deals';

function getDeals(): Promise<Deal[]> {
    return fetch(`${baseUrl}`).then(x => x.json());
}

function getDeal(id: Guid): Promise<Deal> {
    return fetch(`${baseUrl}/${id}`).then(x => x.json());
}

function getDealsOfClient(clientId: Guid): Promise<Deal[]> {
    return fetch(`${baseUrl}/forClient=${clientId}`).then(x => x.json());
}

function getDealsOfBeat(beatId: Guid): Promise<Deal[]> {
    return fetch(`${baseUrl}/forBeat=${beatId}`).then(x => x.json());
}

function postDeal(deal: Deal, clientId: Guid): Promise<void> {
    return fetch(`${baseUrl}/client=${clientId}`, {
        method: 'POST',
        headers: {'content-type': 'application/json'},
        body: JSON.stringify(deal)
    }).then();
}

function putDeal(deal: Deal, id: Guid): Promise<void> {
    return fetch(`${baseUrl}/${id}`, {
        method: 'PUT',
        headers: {'content-type': 'application/json'},
        body: JSON.stringify(deal)
    }).then();
}

function deleteDeal(id: Guid): Promise<void> {
    return fetch(`${baseUrl}/${id}`, {
        method: 'DELETE',
    }).then();
}

export const dealApi = {
    getDeals,
    getDeal,
    getDealsOfClient,
    getDealsOfBeat,
    postDeal,
    putDeal,
    deleteDeal
} as const;

export interface Deal{
    Id: Guid,
    ClientId: Guid,
    BeatId: Guid,
    ContractId: Guid,
    Beat: Beat,
    DealType: DealType,
    Price: number
}

export enum DealType{
    Lease,
    Sale
}