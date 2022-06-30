import React from "react";
import {Button, Table} from "reactstrap";
import ReactAudioPlayer from "react-audio-player";
import {Link} from "react-router-dom";
import {Beat} from "./AllBeats";


export const Bag: React.FC = ({}) => {
    const [beats, setBeats] = React.useState<Beat[]>()

    React.useEffect(() => {
        getBeats().then(setBeats);
    }, []);

    return(
        <>
            <h3 style={{textAlign: "center"}}>Выбранные биты</h3>
            <Table >
                <thead>
                <tr>
                    <th> </th>
                    <th>Название</th>
                    <th>Тип сделки</th>
                    <th>Цена</th>
                    <th> </th>
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
                            <td>Лизинг</td>
                            <td>{b.priceToLease}</td>
                            <th>Удалить из корзины </th>
                        </tr>
                    ))
                }
                </tbody>
            </Table>
            <Button>Купить</Button>
        </>
    )

    function getBeats() : Promise<Beat[]>{
        return fetch('/beats').then(x => x.json());
    }
}