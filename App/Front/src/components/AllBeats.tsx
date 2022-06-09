import React from "react";
import {Table} from "reactstrap";

export const AllBeats: React.FC = ({}) => {
    const [beats, setBeats] = React.useState<Beat[]>()

    React.useEffect(() => {
        getBeats().then(setBeats);
    }, []);

    return(
        <>
            <h3 style={{textAlign: "center"}}>Все биты</h3>
            <Table>
                <thead>
                <tr>
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
                            <td>{b.name}</td>
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
        return fetch('/beats').then(x => x.json())
    }

    interface Beat {
        id: Guid;
        name: string;
        priceToBuy: number;
        priceToLease: number;
        bpm: number;
    }
}