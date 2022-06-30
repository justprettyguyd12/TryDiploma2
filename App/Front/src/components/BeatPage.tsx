import {Beat, beatsApi, BeatStatus} from "../api/beatsApi";
import React, {Props} from "react";
import {Button, Table} from "reactstrap"; 
import {useParams} from "react-router-dom";
import ReactAudioPlayer from "react-audio-player";

export const BeatPage: React.FC = ({}) => {
    const {id} = useParams();
    const[beat, setBeat] = React.useState<Beat>();

    React.useEffect(() => {
        getBeat().then(setBeat);
    }, []);

    return(
        <>
            <h3 style={{textAlign: "center"}}>{beat?.name}</h3>
            <div className="container">
                <ReactAudioPlayer 
                    src={`${beat?.name}/demo/${beat?.name.toLowerCase()} demo.mp3`} 
                    className="w-100"
                    controls/>
                <div className="row align-items-start">
                    <div className="col-6">
                        <Table className="table-hover">
                            <tbody className="border">
                            <tr>
                                <td className="col-6 text-end fw-bolder">
                                    Темп
                                </td>
                                <td className="col-3 font-italic">{beat?.bpm} bpm</td>
                            </tr>
                            <tr>
                                <td className="col-6 text-end fw-bolder">
                                    Купить в лизинг
                                </td>
                                <td className="col-3 font-italic">{beat?.priceToLease}</td>
                            </tr>
                            <tr>
                                <td className="col-6 text-end fw-bolder">
                                    Купить в эксклюзив
                                </td>
                                <td className="col-3 font-italic">{beat?.priceToBuy}</td>
                            </tr>
                            </tbody>    
                        </Table>
                    </div>
                    <div className="col-6 border p-0" style={{height: 123}}>
                        <p className="fst-italic center text-center">{beat?.description}</p>
                    </div>
                </div>
                <Button>Купить!</Button>
            </div>
        </>
    )
    
    function getBeat(): Promise<Beat>{
        return fetch(`${id}`).then(x => x.json());
    }
}