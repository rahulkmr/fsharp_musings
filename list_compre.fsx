let x =
  [ let negate x = -x
    for i in 1.. 10 do
      if i %2 = 0 then
        yield negate i
      else
        yield i    ]
