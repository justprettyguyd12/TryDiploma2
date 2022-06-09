import React from 'react';
import { Table } from 'reactstrap';
import styles from './App.scss';
export var App = function (_a) {
    var _b = React.useState(), beats = _b[0], setBeats = _b[1];
    React.useEffect(function () {
        getBeats().then(setBeats);
    }, []);
    return (React.createElement(React.Fragment, null,
        React.createElement("div", { className: styles.root, style: { alignItems: "center" } },
            React.createElement("h1", { style: { textAlign: "center" } }, "Home"),
            React.createElement(Table, null,
                React.createElement("thead", null,
                    React.createElement("tr", null,
                        React.createElement("th", null, "\u0412\u0441\u0435 \u0431\u0438\u0442\u044B")),
                    React.createElement("tr", null,
                        React.createElement("th", null, "\u041D\u0430\u0437\u0432\u0430\u043D\u0438\u0435"),
                        React.createElement("th", null, "\u0426\u0435\u043D\u0430 \u0437\u0430 \u044D\u043A\u0441\u043A\u043B\u044E\u0437\u0438\u0432"),
                        React.createElement("th", null, "\u0426\u0435\u043D\u0430 \u0437\u0430 \u043B\u0438\u0437\u0438\u043D\u0433"),
                        React.createElement("th", null, "\u0422\u0435\u043C\u043F"))),
                React.createElement("tbody", null, beats === null || beats === void 0 ? void 0 : beats.map(function (b) { return (React.createElement("tr", null,
                    React.createElement("td", null, b.name),
                    React.createElement("td", null, b.priceToBuy),
                    React.createElement("td", null, b.priceToLease),
                    React.createElement("td", null, b.bpm))); }))))));
    function getBeats() {
        return fetch('/beats').then(function (x) { return x.json(); });
    }
};
