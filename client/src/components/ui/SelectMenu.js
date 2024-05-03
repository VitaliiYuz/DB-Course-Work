import { Fragment, useEffect, useState } from 'react'
import { Listbox, Transition } from '@headlessui/react'
import { CheckIcon, ChevronUpDownIcon } from '@heroicons/react/20/solid'

function classNames(...classes) {
    return classes.filter(Boolean).join(' ')
}

const SelectMenu = ({ selected, setSelected, items }) => {

    if(selected === undefined && items.length > 0){
        setSelected(items[0]);
    }

    return (
        <></>
    );
};

export default SelectMenu;