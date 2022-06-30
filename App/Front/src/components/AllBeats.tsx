import React from "react";
import {NavLink, Table} from "reactstrap";
import ReactAudioPlayer from "react-audio-player";
import {Link} from "react-router-dom";

export const AllBeats: React.FC = ({}) => {
    const [beats, setBeats] = React.useState<Beat[]>()

    React.useEffect(() => {
        getBeats().then(setBeats);
    }, []);

    return(
        <>
            <h3 style={{textAlign: "center"}}>Все биты</h3>
            <Table >
                <thead>
                <tr>
                    <th> </th>
                    <th>Название</th>
                    <th>Цена за эксклюзив</th>
                    <th>Цена за лизинг</th>
                    <th>Темп</th>
                </tr>
                </thead>
                <tbody>
                {
                    beats?.map((b) => (
                            <tr key={b.id}>
                                <td>
                                    <ReactAudioPlayer src={`beats/${b.name}/demo/${b.name.toLowerCase()} demo.mp3`} controls/>
                                </td>
                                <td>
                                    <Link className="text-dark" style={{ textDecoration: 'none' }} to={`beats/${b.id}`}>{b.name}</Link>
                                </td>
                                <td>{b.priceToBuy}</td>
                                <td>{b.priceToLease}</td>
                                <td>{b.bpm}</td>
                            </tr>
                    ))
                }
                </tbody>
            </Table>
        </>
    )

    function getBeats() : Promise<Beat[]>{
        return fetch('/beats').then(x => x.json());
    }
}

export interface Beat {
    id: Guid;
    name: string;
    priceToBuy: number;
    priceToLease: number;
    bpm: number;
}