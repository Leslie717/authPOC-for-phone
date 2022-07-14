import React, { useState, useEffect } from 'react';
import auth from '@react-native-firebase/auth';
import { Button, TextInput, View, Text } from 'react-native';




const InputBasic = (props) => {

    // console.log('input props', props)
    // const [input, setInput] = useState('');
    // const handleInputVal = (val) => {
    //     props.setInputVal(val);
    // }

    return (
        <View style={{ width: '80%', }}>

            <Text style={{ marginVertical: 5, fontSize: 16, color: 'black' }}>{props.label}</Text>
            <TextInput
                style={{
                    width: '100%',
                    backgroundColor: 'white',
                    borderColor: 'black',
                    borderWidth: 1,
                    borderRadius: 10,
                    marginVertical: 5,
                    padding: 10
                }}
                placeholder={props.placeholder}
                value={props.inputVal}
                onChangeText={val => props.setInputVal(val)}
                keyboardType={(props.type != '') && props.type}
            />
        </View>
    );
}

export default InputBasic;