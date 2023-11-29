import React from 'react'
import useStateContext from '../hooks/useStateContext'

export default function Question() {
  const { context, setContext } = useStateContext()
  console.log(context)
  return <div>Question</div>
}
