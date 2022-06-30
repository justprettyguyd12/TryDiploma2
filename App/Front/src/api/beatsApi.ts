const baseUrl = '/beats';

function getBeat(id: Guid): Promise<JSON>{
    return fetch(`${baseUrl}/${id}`).then(x => x.json());
}

function getBeats(): Promise<Beat[]> {
    return fetch(baseUrl).then(x => x.json());
}

function getBeatsForFunnel(funnelId: Guid): Promise<Beat[]> {
    return fetch(`${baseUrl}/funnel=${funnelId}`).then(x => x.json());
}

function getBeatsForSection(sectionId: Guid): Promise<Beat[]> {
    return fetch(`${baseUrl}/section=${sectionId}`).then(x => x.json());
}

function postBeat(beat: Beat, sectionId: Guid): Promise<void> {
    return fetch(`${baseUrl}/section=${sectionId}`, {
        method: 'POST',
        headers: {'content-type': 'application/json'},
        body: JSON.stringify(beat)
    }).then();
}

function putBeat(id: Guid, beat: Beat): Promise<void> {
    return fetch(`${baseUrl}/${id}`, {
        method: 'PUT',
        headers: {'content-type': 'application/json'},
        body: JSON.stringify(beat)
    }).then();
}

function deleteBeat(id: Guid): Promise<void> {
    return fetch(`${baseUrl}/${id}`, {
        method: 'DELETE',
    }).then();
}

export const beatsApi = {
    getBeat,
    getBeats,
    getBeatsForFunnel,
    getBeatsForSection,
    postBeat,
    putBeat,
    deleteBeat
} as const;

export interface Beat{
    id: Guid;
    sectionId: Guid;
    name: string;
    description: string;
    pathToDemo: string;
    pathToWav: string;
    pathToTrackout: string;
    priceToBuy: number;
    priceToLease: number;
    bpm: number;
    status: BeatStatus;
}

export enum BeatStatus{
    InSale,
    InLease,
    Sold
}